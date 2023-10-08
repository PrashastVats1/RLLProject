import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DistributionCentre } from 'src/app/models/distribution-center.model';
import { DistributioncentreService } from 'src/app/services/distributioncentre.service';

@Component({
  selector: 'app-add-center',
  templateUrl: './add-center.component.html',
  styleUrls: ['./add-center.component.css'],
})
export class AddCenterComponent implements OnInit {
  addCentre: DistributionCentre = {
    centerId: 0,
    centreName: '',
    city: '',
  };

  constructor(
    private distributionService: DistributioncentreService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  addVaccineCentre() {
    console.log(this.addCentre);
    this.distributionService.addCenter(this.addCentre).subscribe({
      next: (distributionCenter) => {
        console.log(distributionCenter);
        this.toastr.success('Center Added Successfully', 'Success!');
        this.router.navigate(['distribution']);
      },
      error: (response: any) => {
        console.log(response);
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
