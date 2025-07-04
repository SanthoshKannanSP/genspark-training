import { TestBed } from '@angular/core/testing';
import { PaymentService } from './payment-service';
import { Product } from '../models/product-model';

describe('PaymentService', () => {
  let service: PaymentService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PaymentService],
    });

    service = TestBed.inject(PaymentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should set product correctly', (done) => {
    const mockProduct = new Product();
    mockProduct.title = 'Test Product';
    mockProduct.price = 100;

    service.setProduct(mockProduct);

    service.product$.subscribe((product) => {
      expect(product).toEqual(mockProduct);
      done();
    });
  });
});
