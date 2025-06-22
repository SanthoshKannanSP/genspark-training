import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeroPage } from './hero-page/hero-page';
import { SignUpChoosePage } from './sign-up-choose-page/sign-up-choose-page';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeroPage, SignUpChoosePage],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected title = 'AttendanceUI';
}
