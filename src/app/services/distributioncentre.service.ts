import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DistributionCentre } from '../models/distribution-center.model';

@Injectable({
  providedIn: 'root',
})
export class DistributioncentreService {
  private baseUrl: string = 'http://localhost:5063/api/';
  constructor(private http: HttpClient) {}

  getAllCentres(): Observable<DistributionCentre[]> {
    return this.http.get<DistributionCentre[]>(this.baseUrl + 'VaccineCentres');
  }

  addCenter(addCenter: DistributionCentre): Observable<DistributionCentre> {
    return this.http.post<DistributionCentre>(
      this.baseUrl + 'VaccineCentres',
      addCenter
    );
  }

  getCenter(id: string): Observable<DistributionCentre> {
    return this.http.get<DistributionCentre>(
      this.baseUrl + 'VaccineCentres/' + id
    );
  }

  updateCenter(
    id: number,
    updateCenter: DistributionCentre
  ): Observable<DistributionCentre> {
    return this.http.put<DistributionCentre>(
      this.baseUrl + 'VaccineCentres/' + id,
      updateCenter
    );
  }

  deleteCenter(id: number): Observable<DistributionCentre> {
    return this.http.delete<DistributionCentre>(
      this.baseUrl + 'VaccineCentres/' + id
    );
  }
}
