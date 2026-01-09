import { Component } from '@angular/core';

import { NavbarComponent } from '../navbar/navbar.component';
import { HeroComponent } from '../hero/hero.component';
import { AboutComponent } from '../about/about.component';
import { ContactComponent } from '../contact/contact.component';
import { CareersComponent } from '../careers/careers.component';
import { ServicesComponent } from '../services/services.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-home',
  imports: [
    NavbarComponent,
    HeroComponent,
    AboutComponent,
    ContactComponent,
    CareersComponent,
    ServicesComponent,
    FooterComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
