import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DistributionCentre } from 'src/app/models/distribution-center.model';
import { DistributioncentreService } from 'src/app/services/distributioncentre.service';

@Component({
  selector: 'app-distribution',
  templateUrl: './distribution.component.html',
  styleUrls: ['./distribution.component.css'],
})
export class DistributionComponent implements OnInit {
  distributionCenter: DistributionCentre[] = [];
  searchTerm: string = '';

  // Getter to return filtered distribution centers based on search term
  get filteredCenters(): DistributionCentre[] {
    if (!this.searchTerm) {
      return this.distributionCenter;
    }
    return this.distributionCenter.filter(
      (center) =>
        center.centreName
          .toLowerCase()
          .includes(this.searchTerm.toLowerCase()) ||
        center.city.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  constructor(
    private centerService: DistributioncentreService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.centerService.getAllCentres().subscribe({
      next: (distributionCenter) => {
        this.distributionCenter = distributionCenter;
      },
      error: (response) => {
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
