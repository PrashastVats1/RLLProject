import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { VaccineStock } from 'src/app/models/stock-vaccine.model';
import { StockvaccineService } from 'src/app/services/stockvaccine.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css'],
})
export class InventoryComponent implements OnInit {
  vaccineStock: VaccineStock[] = [];

  constructor(
    private stockVaccine: StockvaccineService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.stockVaccine.getAllVaccines().subscribe({
      next: (vaccineStock) => {
        console.log(vaccineStock);
        this.vaccineStock = vaccineStock;
      },
      error: (response) => {
        console.log(response);
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
