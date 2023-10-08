import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { VaccineStock } from 'src/app/models/stock-vaccine.model';
import { StockvaccineService } from 'src/app/services/stockvaccine.service';

@Component({
  selector: 'app-add-vaccine',
  templateUrl: './add-vaccine.component.html',
  styleUrls: ['./add-vaccine.component.css'],
})
export class AddVaccineComponent implements OnInit {
  addVaccine: VaccineStock = {
    id: 0,
    vaccineName: '',
    manufacturer: '',
    expiryDate: new Date(),
    stock: 0,
  };

  constructor(
    private vaccineService: StockvaccineService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  addVaccineStock() {
    console.log(this.addVaccine);
    this.vaccineService.addVaccineStock(this.addVaccine).subscribe({
      next: (vaccineStock) => {
        console.log(vaccineStock);
        this.toastr.success('Vaccine Added Successfully', 'Success!');
        this.router.navigate(['inventory']);
      },
      error: (response: any) => {
        console.log(response);
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
