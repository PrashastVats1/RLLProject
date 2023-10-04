import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class StockvaccineService {
  private baseUrl: string = 'http://localhost:5063/api/VaccineStocks';

  constructor(private http: HttpClient, private router: Router) {}

  // Fetch all Vaccine Stocks
  getVaccineStocks() {
    return this.http.get<any[]>(`${this.baseUrl}`);
  }

  // Fetch a single Vaccine Stock by ID
  getVaccineStock(id: number) {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  // Add a new Vaccine Stock
  addVaccineStock(vaccineStockObj: any) {
    return this.http.post<any>(`${this.baseUrl}`, vaccineStockObj);
  }

  // Update an existing Vaccine Stock
  updateVaccineStock(id: number, vaccineStockObj: any) {
    return this.http.put<any>(`${this.baseUrl}/${id}`, vaccineStockObj);
  }

  // Delete a Vaccine Stock by ID
  deleteVaccineStock(id: number) {
    return this.http.delete<any>(`${this.baseUrl}/${id}`);
  }
}
