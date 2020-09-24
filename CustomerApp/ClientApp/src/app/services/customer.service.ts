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

  getCustomerById(id: number): Observable<Customer> {
    return this.http.get<Customer>(this.Api + 'api/Customer/' + id);
  }

  updateById(id: number, customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(this.Api + 'api/Customer/' + id, customer);
  }

  addNew(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.Api + 'api/Customer', customer);
  }

  deleteById(id: number): Observable<Customer[]> {
    return this.http.delete<Customer[]>(this.Api + 'api/Customer/' + id);
  }

  sortCustomer(sortBy: string, sortByValue: number): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.Api + 'api/Customer/SortCustomer/' + sortBy + '/' + sortByValue);
  }

  searchCustomer(searchBy: string, value: string): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.Api + 'api/Customer/GetValueSearch/' + searchBy + '/' + value);
  }

}
