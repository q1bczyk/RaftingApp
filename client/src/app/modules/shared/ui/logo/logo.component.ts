import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-logo',
  templateUrl: './logo.component.html',
  styleUrl: './logo.component.scss',
  standalone: true,
  imports: []
})
export class LogoComponent {
  @Input() size : number | undefined = undefined;
  @Input() fontSize : number | undefined = undefined;
  @Input() fontColor : string | undefined = undefined;
}
