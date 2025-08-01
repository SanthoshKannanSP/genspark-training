import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import {
  RECAPTCHA_V3_SITE_KEY,
  RecaptchaFormsModule,
  RecaptchaModule,
  ReCaptchaV3Service,
} from 'ng-recaptcha';

@Component({
  selector: 'app-contact-us',
  imports: [ReactiveFormsModule, RouterLink, RecaptchaModule, FormsModule],
  templateUrl: './contact-us.html',
  styleUrl: './contact-us.css',
})
export class ContactUs {
  token: string | null = null;
  contactForm: FormGroup;

  public resolved(captchaResponse: string) {
    this.token = captchaResponse;
  }

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.contactForm = this.fb.group({
      cusName: ['', Validators.required],
      cusPhone: ['', Validators.required],
      cusEmail: ['', Validators.required],
      cusContent: ['', Validators.required],
    });
  }

  isDisabled() {
    if (this.contactForm.valid && this.token) return false;
    return true;
  }

  onSubmit() {
    this.http
      .post('http://localhost:5288/api/ContactUs', {
        ...this.contactForm.value,
        recaptchaResponse: this.token,
      })
      .subscribe({
        next: (res: any) => {
          alert(res.message);
          this.router.navigate(['/home']);
        },
        error: (err) => console.log(err),
      });
  }
}
