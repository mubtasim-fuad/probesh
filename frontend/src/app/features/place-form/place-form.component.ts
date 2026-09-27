import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PlaceApiService } from '../../core/place-api.service';
import {
  CreatePlaceRequest,
  PLACE_CATEGORIES,
  Place,
  PlaceCategory,
} from '../../models/place.model';

@Component({
  selector: 'app-place-form',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './place-form.component.html',
  styleUrl: './place-form.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlaceFormComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly api = inject(PlaceApiService);

  readonly categories = PLACE_CATEGORIES;
  readonly submitting = signal(false);
  readonly errorMessage = signal('');
  readonly submittedPlace = signal<Place | null>(null);

  readonly accessibilityQuestions = [
    {
      control: 'stepFreeEntrance',
      label: 'Step-free entrance',
      hint: 'A person can enter without using stairs.',
    },
    {
      control: 'accessibleToilet',
      label: 'Accessible toilet',
      hint: 'Enough space, grab rails, and a usable doorway.',
    },
    {
      control: 'elevator',
      label: 'Elevator',
      hint: 'Needed only when public areas are on multiple floors.',
    },
    {
      control: 'wideDoorways',
      label: 'Wide doorways',
      hint: 'Main routes appear usable by a wheelchair.',
    },
    {
      control: 'accessibleParking',
      label: 'Accessible parking',
      hint: 'A designated or practical nearby parking space exists.',
    },
    {
      control: 'staffAssistance',
      label: 'Staff assistance',
      hint: 'Staff can provide practical accessibility support.',
    },
  ] as const;

  readonly form = this.formBuilder.nonNullable.group({
    name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(120)]],
    category: ['Healthcare'],
    address: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(240)]],
    area: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(80)]],
    city: ['Dhaka', [Validators.required, Validators.minLength(2), Validators.maxLength(80)]],
    latitude: [23.8103, [Validators.required, Validators.min(-90), Validators.max(90)]],
    longitude: [90.4125, [Validators.required, Validators.min(-180), Validators.max(180)]],
    description: ['', Validators.maxLength(800)],
    stepFreeEntrance: ['unknown'],
    accessibleToilet: ['unknown'],
    elevator: ['unknown'],
    wideDoorways: ['unknown'],
    accessibleParking: ['unknown'],
    staffAssistance: ['unknown'],
    notes: ['', Validators.maxLength(800)],
    evidencePhotoUrl: [
      '',
      [Validators.maxLength(500), Validators.pattern(/^$|^https?:\/\/.+/i)],
    ],
  });

  submit(): void {
    this.errorMessage.set('');
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.errorMessage.set('Please correct the highlighted fields before submitting.');
      return;
    }

    const value = this.form.getRawValue();
    const request: CreatePlaceRequest = {
      name: value.name.trim(),
      category: value.category as PlaceCategory,
      address: value.address.trim(),
      area: value.area.trim(),
      city: value.city.trim(),
      latitude: Number(value.latitude),
      longitude: Number(value.longitude),
      description: this.cleanOptional(value.description),
      audit: {
        stepFreeEntrance: this.toNullableBoolean(value.stepFreeEntrance),
        accessibleToilet: this.toNullableBoolean(value.accessibleToilet),
        elevator: this.toNullableBoolean(value.elevator),
        wideDoorways: this.toNullableBoolean(value.wideDoorways),
        accessibleParking: this.toNullableBoolean(value.accessibleParking),
        staffAssistance: this.toNullableBoolean(value.staffAssistance),
        notes: this.cleanOptional(value.notes),
        evidencePhotoUrl: this.cleanOptional(value.evidencePhotoUrl),
      },
    };

    this.submitting.set(true);
    this.api.createPlace(request).subscribe({
      next: (place) => {
        this.submittedPlace.set(place);
        this.submitting.set(false);
        this.form.disable();
      },
      error: (error: HttpErrorResponse) => {
        this.errorMessage.set(
          error.status === 0
            ? 'The API could not be reached. Start the ASP.NET Core project on port 5074.'
            : 'The API rejected this submission. Check the fields and try again.',
        );
        this.submitting.set(false);
      },
    });
  }

  hasError(controlName: string): boolean {
    const control = this.form.get(controlName);
    return Boolean(control && control.invalid && control.touched);
  }

  private toNullableBoolean(value: string): boolean | null {
    if (value === 'yes') return true;
    if (value === 'no') return false;
    return null;
  }

  private cleanOptional(value: string): string | null {
    const cleaned = value.trim();
    return cleaned.length > 0 ? cleaned : null;
  }
}
