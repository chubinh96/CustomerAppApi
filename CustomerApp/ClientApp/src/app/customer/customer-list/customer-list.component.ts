import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../model/customer.model';
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

  myAppUrl = '';

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

  onSearch(option, value) {
    if (value === "") {
      return this.http.get<Customer[]>(this.myAppUrl + 'api/Customer/GetCustomer').subscribe(result => {
        this.customers = result;
      }, error => console.error(error));
    } else {
      if (option == "name") {
        return this.http.get<Customer[]>(this.myAppUrl + 'api/Customer/SearchByName/' + value).subscribe(result => {
          this.customers = result;
          console.log(result);
          console.log(this.myAppUrl + 'api/Customer/SearchByName/' + value);
        }, error => console.error(error));
      }
      if (option == "country") {
        return this.http.get<Customer[]>(this.myAppUrl + 'api/Customer/SearchByCountry/' + value).subscribe(result => {
          this.customers = result;
          console.log(result);
          console.log(this.myAppUrl + 'api/Customer/SearchByCountry/' + value);
        }, error => console.error(error));
      }
    }
  }

  onDelete(id) {
    console.log(id);
    this.subscription = this.customerService.deleteById(id).subscribe(data => {
      console.log(data);
    });
  }

  fetchData() {
    return this.http.get<Customer[]>(this.myAppUrl + 'api/Customer/GetCustomer').subscribe(result => {
      this.customers = result;
    }, error => console.error(error));
  }

}

//interface Customer {
  //id: number;
  //name: string;
  //country: string;
  //phone: number;
//}
