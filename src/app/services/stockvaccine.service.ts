import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { VaccineStock } from '../models/stock-vaccine.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StockvaccineService {
  private baseUrl: string = 'http://localhost:5063/api/';
  constructor(private http: HttpClient) {}

  getAllVaccines(): Observable<VaccineStock[]> {
    return this.http.get<VaccineStock[]>(this.baseUrl + 'VaccineStocks');
  }

  addVaccineStock(addVaccine: VaccineStock): Observable<VaccineStock> {
    return this.http.post<VaccineStock>(
      this.baseUrl + 'VaccineStocks',
      addVaccine
    );
  }

  getVaccine(id: string): Observable<VaccineStock> {
    return this.http.get<VaccineStock>(this.baseUrl + 'VaccineStocks/' + id);
  }

  updateVaccine(
    id: number,
    updateVaccine: VaccineStock
  ): Observable<VaccineStock> {
    return this.http.put<VaccineStock>(
      this.baseUrl + 'VaccineStocks/' + id,
      updateVaccine
    );
  }

  deleteVaccine(id: number): Observable<VaccineStock> {
    return this.http.delete<VaccineStock>(this.baseUrl + 'VaccineStocks/' + id);
  }
}
