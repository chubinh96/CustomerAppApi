import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Customer } from '../../model/customer';
import { CustomerService } from '../../services/customer.service';
import { error } from 'util';
import { from } from 'rxjs/observable/from';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {
  public title = '';
  public status: boolean;
  currentCustomer: Customer = new Customer();
  subscription: Subscription;
  customerModelId = 0;

  //Base Url
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
      this.customerModelId = Number(params.get("id"));

      this.subscription = this.customerService.getCustomerById(this.customerModelId).subscribe(data => {
        this.currentCustomer = data;
      }, error => console.error(error));

      if (this.customerModelId == 0 ) {
        this.title = 'Add';
        this.status = false; 
      } else {
        this.title = 'Edit';
        this.status = true; 
      }
    });

  }

  onAdd() {
    this.subscription = this.customerService.addNew(this.currentCustomer).subscribe(data => {
      console.log(this.currentCustomer);
      console.log(data);
    }, error => console.error(error));
  }

  onEdit() {
    this.subscription = this.customerService.updateById(this.currentCustomer.id, this.currentCustomer).subscribe(data => {
      console.log(this.currentCustomer);
      console.log(data);
    }, error => console.error(error));
  }

}
