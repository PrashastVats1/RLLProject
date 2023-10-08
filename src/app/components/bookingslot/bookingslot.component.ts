import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BookingSlot } from 'src/app/models/booking-slot.model';
import { AuthService } from 'src/app/services/auth.service';
import { BookingslotService } from 'src/app/services/bookingslot.service';

@Component({
  selector: 'app-bookingslot',
  templateUrl: './bookingslot.component.html',
  styleUrls: ['./bookingslot.component.css'],
})
export class BookingslotComponent implements OnInit {
  bookingSlot: BookingSlot[] = [];
  searchTerm: string = '';
  loggedInUserName!: string;

  // Getter to return filtered vaccines based on search term
  get filteredVaccines(): BookingSlot[] {
    if (!this.searchTerm) {
      return this.bookingSlot;
    }
    return this.bookingSlot.filter((book) =>
      book.vaccineName
        ? book.vaccineName.toLowerCase().includes(this.searchTerm.toLowerCase())
        : false
    );
  }

  constructor(
    private bookingService: BookingslotService,
    private toastr: ToastrService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    this.loggedInUserName = this.auth.getCurrentUserName();
    this.bookingService.getAllVaccines().subscribe({
      next: (bookingSlot) => {
        this.bookingSlot = bookingSlot.filter(
          (booking) => booking.userName === this.loggedInUserName
        );
      },
      error: (response) => {
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
