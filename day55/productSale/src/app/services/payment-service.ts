import { Injectable } from '@angular/core';
import { Product } from '../models/product-model';
import { BehaviorSubject } from 'rxjs';
import { Environment } from '../environment/environment';
declare var Razorpay: any;

@Injectable()
export class PaymentService {
  product = new BehaviorSubject<Product>(new Product());
  product$ = this.product.asObservable();
  paymentStatus = new BehaviorSubject<any>(null);
  paymentStatus$ = this.paymentStatus.asObservable();

  setProduct(product: Product) {
    this.product.next(product);
  }

  pay(title: string, price: number, form: any) {
    const options: any = {
      key: Environment.razorKey,
      amount: price * 100,
      currency: 'INR',
      name: 'Razor Pay Demo',
      description: `Payment for ${title}`,
      prefill: {
        name: form.customerName,
        email: form.email,
        contact: form.contact,
      },
      handler: (response: any) => {
        console.log('Payment success:', response);
        this.paymentStatus.next({
          success: true,
          message: `Payment Successful! Payment Id: ${response.razorpay_payment_id}`,
        });
      },
      modal: {
        ondismiss: () => {
          console.log('Payment popup closed');
          this.paymentStatus.next({
            success: false,
            message: `Payment Failed or Cancelled. Please try again later`,
          });
        },
      },
    };

    const rzp = new Razorpay(options);
    rzp.open();
  }
}
