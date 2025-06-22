import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-past-session-card',
  imports: [],
  templateUrl: './past-session-card.html',
  styleUrl: './past-session-card.css',
})
export class PastSessionCard {
  @Input() title = '';
  @Input() date = '';
  @Input() time = '';
}
