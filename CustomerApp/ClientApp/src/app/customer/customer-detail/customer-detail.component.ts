import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Customer } from '../../model/customer.model';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {
  //currentCustomer: Customer[];
  //currentCustomer: Customer = new Customer();
  currentCustomer: Customer;
  subscription: Subscription;
  customerModelId = 0;

  myAppUrl = '';

  constructor(
    private http: HttpClient,
    private activeRouteService: ActivatedRoute,
    private routerActive: Router,
    private customerService: CustomerService,
    @Inject('BASE_URL') base_url: string
  ) {
    this.myAppUrl = base_url;
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.subscription = this.activeRouteService.paramMap.subscribe(params => {
      let id = params.get("id");
      this.customerModelId = Number(id);
      this.http.get<Customer[]>(this.myAppUrl + 'api/Customer/' + id).subscribe(result => {
        //this.currentCustomer = result;
        //console.log(this.currentCustomer);
      }, error => console.error(error));
    });
  }


  onSubmit(form: NgForm) {
    this.currentCustomer = form.value;
    //this.http.post<Customer[]>(this.myAppUrl + 'api/Customer', this.currentCustomer).subscribe(result => {
      //console.log(result);
      //this.routerActive.navigateByUrl('customer');
    //}, error => console.error(error));

    this.subscription = this.customerService.addNew(this.currentCustomer).subscribe(data => {
      console.log(data);
      //this.routerActive.navigateByUrl('customer');
    });

    console.log(this.currentCustomer);
  }
  

  onAdd(form: NgForm) {
    this.currentCustomer = form.value;
    console.log(this.currentCustomer);
    this.http.post<Customer[]>(this.myAppUrl + 'api/Customer', this.currentCustomer).subscribe(result => {
      //this.routerActive.navigateByUrl('customer');
    }, error => console.error(error));
  }

}
