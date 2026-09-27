import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  OnInit,
  computed,
  inject,
  signal,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { PlaceApiService } from '../../core/place-api.service';
import {
  ACCESSIBILITY_LEVELS,
  AccessibilityAudit,
  AccessibilityLevel,
  PLACE_CATEGORIES,
  Place,
  PlaceCategory,
  PlaceSummary,
} from '../../models/place.model';

interface FeatureRow {
  label: string;
  value: boolean | null;
}

@Component({
  selector: 'app-place-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './place-list.component.html',
  styleUrl: './place-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlaceListComponent implements OnInit {
  private readonly api = inject(PlaceApiService);

  readonly categories = PLACE_CATEGORIES;
  readonly levels = ACCESSIBILITY_LEVELS;
  readonly places = signal<Place[]>([]);
  readonly summary = signal<PlaceSummary | null>(null);
  readonly loading = signal(true);
  readonly errorMessage = signal('');
  readonly search = signal('');
  readonly category = signal<PlaceCategory | 'All'>('All');
  readonly level = signal<AccessibilityLevel | 'All'>('All');

  readonly filteredPlaces = computed(() => {
    const term = this.search().trim().toLowerCase();
    const category = this.category();
    const level = this.level();

    return this.places().filter((place) => {
      const matchesText =
        !term ||
        `${place.name} ${place.area} ${place.city}`.toLowerCase().includes(term);
      const matchesCategory = category === 'All' || place.category === category;
      const matchesLevel = level === 'All' || place.audit.level === level;
      return matchesText && matchesCategory && matchesLevel;
    });
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.errorMessage.set('');

    forkJoin({
      places: this.api.getPlaces(),
      summary: this.api.getSummary(),
    }).subscribe({
      next: ({ places, summary }) => {
        this.places.set(places);
        this.summary.set(summary);
        this.loading.set(false);
      },
      error: () => {
        this.errorMessage.set(
          'The API could not be reached. Start the ASP.NET Core project on port 5074, then retry.',
        );
        this.loading.set(false);
      },
    });
  }

  setCategory(value: string): void {
    this.category.set(value as PlaceCategory | 'All');
  }

  setLevel(value: string): void {
    this.level.set(value as AccessibilityLevel | 'All');
  }

  featureRows(audit: AccessibilityAudit): FeatureRow[] {
    return [
      { label: 'Step-free entrance', value: audit.stepFreeEntrance },
      { label: 'Accessible toilet', value: audit.accessibleToilet },
      { label: 'Elevator', value: audit.elevator },
      { label: 'Wide doorways', value: audit.wideDoorways },
      { label: 'Accessible parking', value: audit.accessibleParking },
      { label: 'Staff assistance', value: audit.staffAssistance },
    ];
  }

  answerLabel(value: boolean | null): string {
    if (value === null) return 'Unknown';
    return value ? 'Yes' : 'No';
  }

  answerClass(value: boolean | null): string {
    if (value === null) return 'answer--unknown';
    return value ? 'answer--yes' : 'answer--no';
  }

  humanize(value: string): string {
    return value.replace(/([a-z])([A-Z])/g, '$1 $2');
  }
}
