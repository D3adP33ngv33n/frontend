import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';

import Prism from '../prism'
export default Prism;

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
