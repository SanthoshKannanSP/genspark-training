import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductsPage } from './products-page';
import { ProductService } from '../services/product-service';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Component } from '@angular/core';

@Component({
  selector: 'app-product',
  template: '',
})
class MockProductComponent {}

describe('Products Component (Standalone)', () => {
  let component: ProductsPage;
  let fixture: ComponentFixture<ProductsPage>;
  let productServiceSpy: jasmine.SpyObj<ProductService>;

  const dummyProductData = {
    products: [
      { id: 1, title: 'Phone' },
      { id: 2, title: 'Laptop' },
    ],
    total: 20,
  };

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('ProductService', ['getProducts']);

    await TestBed.configureTestingModule({
      imports: [ProductsPage, FormsModule],
      providers: [{ provide: ProductService, useValue: spy }],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    }).compileComponents();

    productServiceSpy = TestBed.inject(
      ProductService
    ) as jasmine.SpyObj<ProductService>;
    productServiceSpy.getProducts.and.returnValue(of(dummyProductData));

    fixture = TestBed.createComponent(ProductsPage);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });
});
