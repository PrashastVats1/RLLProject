import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { StockvaccineService } from 'src/app/services/stockvaccine.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css'],
})
export class InventoryComponent implements OnInit {
  vaccineStocks: any[] = [];
  selectedStock: any;

  constructor(
    private stockService: StockvaccineService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.loadVaccineStocks();
  }

  loadVaccineStocks(): void {
    this.stockService.getVaccineStocks().subscribe(
      (stocks) => {
        this.vaccineStocks = stocks;
      },
      (error) => {
        console.error('Error fetching vaccine stocks', error);
      }
    );
  }
  addVaccineStock(newStock: any): void {
    this.stockService.addVaccineStock(newStock).subscribe(
      (addedStock) => {
        this.vaccineStocks.push(addedStock);
      },
      (error) => {
        console.error('Error adding new stock', error);
      }
    );
  }

  updateVaccineStock(id: number, updatedStock: any): void {
    this.stockService.updateVaccineStock(id, updatedStock).subscribe(
      () => {
        const index = this.vaccineStocks.findIndex((stock) => stock.Id === id);
        if (index !== -1) {
          this.vaccineStocks[index] = updatedStock;
        }
      },
      (error) => {
        console.error('Error updating stock', error);
      }
    );
  }

  deleteVaccineStock(id: number): void {
    this.stockService.deleteVaccineStock(id).subscribe(
      () => {
        const index = this.vaccineStocks.findIndex((stock) => stock.Id === id);
        if (index !== -1) {
          this.vaccineStocks.splice(index, 1);
        }
      },
      (error) => {
        console.error('Error deleting stock', error);
      }
    );
  }
  openUpdateForm(stock: any, content: any): void {
    this.selectedStock = stock;
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          if (result === 'Save') {
            this.updateVaccineStock(this.selectedStock.Id, this.selectedStock);
          }
        },
        (reason) => {
          this.handleDismissalReason(reason);
        }
      );
  }
  private handleDismissalReason(reason: any): void {
    if (reason === 'ESC') {
      console.log('Modal was dismissed using ESC key.');
    } else if (reason === 'BACKDROP_CLICK') {
      console.log('Modal was dismissed by backdrop click.');
    } else {
      console.log('Modal was dismissed by another reason:', reason);
    }
  }
}
