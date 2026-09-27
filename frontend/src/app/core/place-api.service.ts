import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CreatePlaceRequest,
  Place,
  PlaceSummary,
} from '../models/place.model';

@Injectable({ providedIn: 'root' })
export class PlaceApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5074/api/places';

  getPlaces(): Observable<Place[]> {
    return this.http.get<Place[]>(this.baseUrl);
  }

  getSummary(): Observable<PlaceSummary> {
    return this.http.get<PlaceSummary>(`${this.baseUrl}/summary`);
  }

  createPlace(request: CreatePlaceRequest): Observable<Place> {
    return this.http.post<Place>(this.baseUrl, request);
  }
}
