import { Injectable, Inject  } from '@angular/core';
import { Customer } from '../model/Customer.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CustomerService {

  Api = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.Api = baseUrl;
  }

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.Api + 'api/Customer/GetCustomer');
  }


}
