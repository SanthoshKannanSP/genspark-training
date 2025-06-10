import { Component, Input } from '@angular/core';
import { CustomerDetail } from '../types/CustomerDetail';

@Component({
  selector: 'app-customer-details',
  imports: [],
  templateUrl: './customer-details.html',
  styleUrl: './customer-details.css'
})
export class CustomerDetails {
  likes : number = 0;
decreaseCount() {
this.likes -=1;
}
increaseCount() {
this.likes +=1;
}
  customerDetailJson : CustomerDetail = {
    id: 1,
    name: "Varun",
    phonenumber: "1234567890",
    email: "varun@gmail.com"
  };
}
