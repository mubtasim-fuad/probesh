import { Routes } from '@angular/router';
import { PlaceFormComponent } from './features/place-form/place-form.component';
import { PlaceListComponent } from './features/place-list/place-list.component';

export const routes: Routes = [
  { path: '', component: PlaceListComponent, title: 'Explore places — Probesh' },
  {
    path: 'places/new',
    component: PlaceFormComponent,
    title: 'Add a place — Probesh',
  },
  { path: '**', redirectTo: '' },
];
