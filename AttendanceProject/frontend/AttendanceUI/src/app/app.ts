import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeroPage } from './hero-page/hero-page';
import { AccountService } from './services/account-service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected title = 'AttendanceUI';
}
