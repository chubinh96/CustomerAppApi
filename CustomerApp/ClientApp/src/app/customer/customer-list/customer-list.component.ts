import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../model/customer';
import { from } from 'rxjs/observable/from'; 
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  public customers: Customer[];
  public subscription: Subscription;

  //Base Url
  myAppUrl = '';

  //Sort Function
  sortBy = 'id';
  sortByValue = 1;
  sortById = 1;
  sortByName = 1;
  sortByCountry = 1;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private customerService: CustomerService
  ) {
    this.myAppUrl = baseUrl;
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.subscription = this.customerService.getAll().subscribe(data => {
      this.customers = data;
    });
  }

  onDelete(id: number, name: string) {
    if (confirm("Do you want to delete " + name)) {
      this.subscription = this.customerService.deleteById(id).subscribe(data => {
        this.customers = data;
      });
      alert("Deleted " + name);
    } else {
      return;
    }
  }

  onSort(sortBy: string) {
    this.sortBy = sortBy;
    if (this.sortBy == 'id') {
      this.sortById = -this.sortById;
      this.sortByName = 1;
      this.sortByCountry = 1;

      this.sortByValue = this.sortById;
      this.subscription = this.customerService.sortCustomer(this.sortBy, this.sortByValue).subscribe(data => {
        console.log(data);
        this.customers = data;
      });
    }
    if (this.sortBy == 'name') {
      this.sortByName = -this.sortByName;
      this.sortById = 1;
      this.sortByCountry = 1;

      this.sortByValue = this.sortByName;
      this.subscription = this.customerService.sortCustomer(this.sortBy, this.sortByValue).subscribe(data => {
        console.log(data);
        this.customers = data;
      });
    }
    if (this.sortBy == 'country') {
      this.sortByCountry = -this.sortByCountry;
      this.sortById = 1;
      this.sortByName = 1;

      this.sortByValue = this.sortByCountry;
      this.subscription = this.customerService.sortCustomer(this.sortBy, this.sortByValue).subscribe(data => {
        console.log(data);
        this.customers = data;
      });
    }
  }

  //Search Function
  onSearch(option, value) {
    if (value === "") {
      this.subscription = this.customerService.getAll().subscribe(data => {
        this.customers = data;
      }, error => console.error(error));
    } else {
      if (option == "name") {
        this.subscription = this.customerService.searchCustomer(option, value).subscribe(data => {
          this.customers = data;
        }, error => console.error(error));
      }
      if (option == "country") {
        this.subscription = this.customerService.searchCustomer(option, value).subscribe(data => {
          this.customers = data;
        }, error => console.error(error));
      }
    }
  }

}
