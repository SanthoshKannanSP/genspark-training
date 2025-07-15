import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationToast } from './notification-toast';
import { of } from 'rxjs';
import { NotificationModal } from '../models/notification-modal';
import { NotificationService } from '../services/notification-service';
import { By } from '@angular/platform-browser';

describe('NotificationToast', () => {
  let component: NotificationToast;
  let fixture: ComponentFixture<NotificationToast>;

  const mockNotifications: NotificationModal[] = [
    {
      message: 'Success message',
      type: 'success',
    },
    {
      message: 'Error message',
      type: 'danger',
    },
  ];

  beforeEach(async () => {
    const notificationSpy = jasmine.createSpyObj('NotificationService', [], {
      messages$: of(mockNotifications),
    });
    await TestBed.configureTestingModule({
      imports: [NotificationToast],
      providers: [{ provide: NotificationService, useValue: notificationSpy }],
    }).compileComponents();

    fixture = TestBed.createComponent(NotificationToast);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display correct number and type of notifications', () => {
    const notificationContainer = fixture.debugElement.query(
      By.css('.toast-container')
    );

    expect(component.notifications.length).toEqual(2);
    expect(notificationContainer.children.length).toEqual(2);

    expect(notificationContainer.children[0].nativeElement).toHaveClass(
      'bg-success'
    );
    expect(notificationContainer.children[0].nativeElement.textContent).toEqual(
      'Success message'
    );

    expect(notificationContainer.children[1].nativeElement).toHaveClass(
      'bg-danger'
    );
    expect(notificationContainer.children[1].nativeElement.textContent).toEqual(
      'Error message'
    );
  });
});
