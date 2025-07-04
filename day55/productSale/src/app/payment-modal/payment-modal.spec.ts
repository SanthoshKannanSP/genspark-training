import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PaymentModal } from './payment-modal';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { PaymentService } from '../services/payment-service';
import { of, BehaviorSubject } from 'rxjs';
import { Product } from '../models/product-model';
import { By } from '@angular/platform-browser';

describe('PaymentModal', () => {
  let component: PaymentModal;
  let fixture: ComponentFixture<PaymentModal>;
  let mockPaymentService: jasmine.SpyObj<PaymentService>;

  const mockProduct: Product = {
    title: 'Test Product',
    price: 500,
  } as Product;

  const mockPaymentStatus = {
    success: true,
    message: 'Payment Successful! Payment Id: pay_abc123',
  };

  beforeEach(async () => {
    mockPaymentService = jasmine.createSpyObj('PaymentService', ['pay'], {
      product$: new BehaviorSubject<Product>(mockProduct),
      paymentStatus$: new BehaviorSubject<any>(null),
    });

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, FormsModule],
      providers: [
        PaymentModal,
        { provide: PaymentService, useValue: mockPaymentService },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(PaymentModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should render product title and price', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.modal-title')?.textContent).toContain(
      'Pay for Test Product'
    );
    expect(compiled.querySelector('.text-primary')?.textContent).toContain(
      '₹500'
    );
  });

  it('should mark form fields as touched if form is invalid on submit', () => {
    spyOn(component.paymentForm, 'markAllAsTouched');
    component.onSubmit();
    expect(component.paymentForm.markAllAsTouched).toHaveBeenCalled();
  });

  it('should call paymentService.pay when form is valid', () => {
    component.paymentForm.setValue({
      customerName: 'John Doe',
      email: 'john@example.com',
      contact: '9876543210',
    });

    component.onSubmit();

    expect(mockPaymentService.pay).toHaveBeenCalledWith('Test Product', 500, {
      customerName: 'John Doe',
      email: 'john@example.com',
      contact: '9876543210',
    });
  });

  it('should show validation messages when fields are touched and invalid', () => {
    const nameControl = component.paymentForm.get('customerName');
    nameControl?.markAsTouched();
    nameControl?.setValue('');

    fixture.detectChanges();

    const nameError = fixture.debugElement.query(By.css('.text-danger.small'));
    expect(nameError.nativeElement.textContent).toContain('Name is required');
  });
});
