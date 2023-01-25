import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
//NOTA: averiguar por que si encuentra el endpoint de weatherforecast
//en el puerto del cliente y no el de el api creado
//por ello tuve que crear una variable para el endpont del swagger
export function getApiUrl() {
  return "https://localhost:7207/";
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'API_URL', useFactory: getApiUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
