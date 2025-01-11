import { Component } from '@angular/core';
import { PageWrapperComponent } from "../../components/page-wrapper/page-wrapper.component";

@Component({
  selector: 'app-accounts-page',
  standalone: true,
  imports: [PageWrapperComponent],
  templateUrl: './accounts-page.component.html',
  styleUrl: './accounts-page.component.scss'
})
export class AccountsPageComponent {

}
