import { TestBed } from '@angular/core/testing';
import { NotificationService } from './notification-service';
import { NotificationModal } from '../models/notification-modal';
import { HttpClientService } from './http-client-service';

describe('NotificationService', () => {
  let service: NotificationService;

  beforeEach(() => {
    const apiSpy = {
      getUsername: jasmine
        .createSpy('getUsername')
        .and.returnValue('johndoe@gmail.com'),
    };

    TestBed.configureTestingModule({
      providers: [
        NotificationService,
        { provide: HttpClientService, useValue: apiSpy },
      ],
    });
    service = TestBed.inject(NotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('addNotification', () => {
    it('should add notification', () => {
      const notification: NotificationModal = {
        type: 'success',
        message: 'Test notification',
      };

      let emitted: NotificationModal[] = [];
      service.messages$.subscribe((messages) => {
        emitted = messages;
      });

      service.addNotification(notification);

      expect(emitted.length).toBe(1);
      expect(emitted[0]).toEqual(notification);
    });
  });
});
