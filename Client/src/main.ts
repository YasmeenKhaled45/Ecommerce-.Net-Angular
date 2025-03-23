import { appConfig } from 'C:/Users/omare/OneDrive/Desktop/Demo/Skint/Client/src/app/app.config';
import { AppComponent } from './app/app.component';
import { bootstrapApplication } from '@angular/platform-browser';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
