import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookingSlot } from '../models/booking-slot.model';

@Injectable({
  providedIn: 'root',
})
export class BookingslotService {
  private baseUrl: string = 'http://localhost:5063/api/';
  constructor(private http: HttpClient) {}

  getAllVaccines(): Observable<BookingSlot[]> {
    return this.http.get<BookingSlot[]>(this.baseUrl + 'BookingSlots');
  }

  addVaccineStock(addSlot: BookingSlot): Observable<BookingSlot> {
    return this.http.post<BookingSlot>(this.baseUrl + 'BookingSlots', addSlot);
  }

  getVaccine(id: string): Observable<BookingSlot> {
    return this.http.get<BookingSlot>(this.baseUrl + 'BookingSlots/' + id);
  }

  updateVaccine(id: number, updateSlot: BookingSlot): Observable<BookingSlot> {
    return this.http.put<BookingSlot>(
      this.baseUrl + 'BookingSlots/' + id,
      updateSlot
    );
  }

  deleteVaccine(id: number): Observable<BookingSlot> {
    return this.http.delete<BookingSlot>(this.baseUrl + 'BookingSlots/' + id);
  }
}
