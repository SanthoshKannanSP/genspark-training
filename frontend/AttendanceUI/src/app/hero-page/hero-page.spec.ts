import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroPage } from './hero-page';
import {
  ActivatedRoute,
  provideRouter,
  RouterLink,
  RouterModule,
} from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('HeroPage', () => {
  let component: HeroPage;
  let fixture: ComponentFixture<HeroPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HeroPage, RouterTestingModule, HttpClientTestingModule],
    }).compileComponents();

    fixture = TestBed.createComponent(HeroPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
