import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl: string = 'http://localhost:5063/api/User';

  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get<any>(`${this.baseUrl}`);
  }
}
