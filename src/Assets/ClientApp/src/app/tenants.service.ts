import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TenantsService {

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient
  ) { }

  async getTenantsMy(): Promise<any> {
    const client = await this.authService.getAuth0Client();
    const token = await client.getTokenSilently();

    return this.httpClient
      .get('/api/tenants/my', {
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
      .toPromise();
  }

}
