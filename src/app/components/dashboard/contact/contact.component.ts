import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
})
export class ContactComponent implements OnInit {
  formData: any = {
    name: '',
    email: '',
    message: '',
  };

  constructor() {}

  ngOnInit(): void {}

  onSubmit(form: any): void {
    console.log('Form Data:', this.formData);
    form.reset();
  }
}
