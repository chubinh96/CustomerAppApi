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

  getAll2(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.Api + 'api/Customer/getAllCustomer');
  }

  addNew(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.Api + 'api/Customer', customer);
  }

  deleteById(id: number): Observable<Customer> {
    return this.http.delete<Customer>(this.Api + 'api/Customer/${id}');
  }

}
