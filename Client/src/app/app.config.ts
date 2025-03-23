import { APP_INITIALIZER, ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './Core/Interceptors/error.interceptor';
import { loadingInterceptor } from './Core/Interceptors/loading.interceptor';
import { InitService } from './Core/Services/init.service';
import { from, lastValueFrom, Observable, of } from 'rxjs';



export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([errorInterceptor, loadingInterceptor]))
    
  ]
};




