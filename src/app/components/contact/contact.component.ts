import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  imports: [ReactiveFormsModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss'
})
export class ContactComponent {
  contactForm: FormGroup;
  submitted = false;
  
  constructor(private fb: FormBuilder) {
    this.contactForm = this.fb.group({
      purpose: ['', Validators.required],
      message: ['', [Validators.required, Validators.minLength(10)]],
      secureChannel: [false]
    });
  }
  
  onSubmit() {
    if (this.contactForm.valid) {
      this.submitted = true; // Set to true when form is submitted
      
      setTimeout(() => {
        alert('Request submitted. You will be contacted through secure channels if approved.');
        this.contactForm.reset();
        this.submitted = false;
      }, 1000);
    }
  }
}
