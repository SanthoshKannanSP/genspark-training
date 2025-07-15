import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountManagement } from './account-management';
import { StudentModel } from '../../models/student-model';
import { of } from 'rxjs';
import { AccountService } from '../../services/account-service';
import { HttpClientService } from '../../services/http-client-service';

describe('AccountManagement', () => {
  let component: AccountManagement;
  let fixture: ComponentFixture<AccountManagement>;
  let mockAccountService: jasmine.SpyObj<AccountService>;
  let mockHttpClientService: jasmine.SpyObj<HttpClientService>;

  const mockStudentAccount: StudentModel = {
    name: 'John Doe',
    dateOfBirth: '2025-01-02',
    gender: 'Male',
  };

  beforeEach(async () => {
    const accountSpy = jasmine.createSpyObj('AccountService', [], {
      account$: of(mockStudentAccount),
    });
    const apiSpy = jasmine.createSpyObj('HttpClientService', ['hasRole']);

    await TestBed.configureTestingModule({
      imports: [AccountManagement],
      providers: [
        { provide: AccountService, useValue: accountSpy },
        { provide: HttpClientService, useValue: apiSpy },
      ],
    }).compileComponents();

    mockAccountService = TestBed.inject(
      AccountService
    ) as jasmine.SpyObj<AccountService>;
    mockHttpClientService = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
    mockHttpClientService.hasRole.and.returnValue(true);

    fixture = TestBed.createComponent(AccountManagement);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create', () => {
    mockHttpClientService.hasRole.and.returnValue(true);
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });
});
