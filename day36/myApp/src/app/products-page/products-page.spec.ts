import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsPage } from './products-page';
import { ProductService } from '../services/product-service';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('ProductsPage', () => {
  let component: ProductsPage;
  let fixture: ComponentFixture<ProductsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductsPage],
      providers: [ProductService, provideHttpClientTesting()],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
