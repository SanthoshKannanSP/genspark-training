import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Sidebar } from './sidebar';
import { HttpClientService } from '../services/http-client-service';
import { Router } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('Sidebar', () => {
  let component: Sidebar;
  let fixture: ComponentFixture<Sidebar>;

  beforeEach(async () => {
    const apiSpy = jasmine.createSpyObj('HttpClientService', [
      'hasRole',
      'logout',
    ]);
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);
    await TestBed.configureTestingModule({
      imports: [Sidebar, HttpClientTestingModule, RouterTestingModule],
      providers: [
        { provide: HttpClientService, useValue: apiSpy },
        { provie: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(Sidebar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
