import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-blik',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './blik.component.html',
  styleUrl: './blik.component.scss'
})
export class BlikComponent {

  blikForm: FormGroup = new FormGroup({
    mail: new FormControl('', [Validators.required, Validators.email]),
    blikCode: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]),
  })

  onSubmit() : void{

  }

}
