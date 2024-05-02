// s3.service.ts

import { Injectable } from '@angular/core';
import S3 from 'aws-sdk/clients/s3';
import { environment } from '../../environments/environments'

@Injectable({
  providedIn: 'root',
})
export class S3Service {
  private s3: S3;

  constructor() {
    this.s3 = new S3({
      accessKeyId: environment.awsAccessKeyId,
      secretAccessKey: environment.awsSecretAccessKey,
      region: environment.awsRegion,
    });
  }

  getObjectUrl(bucket: string, key: string): string {
    return this.s3.getSignedUrl('getObject', {
      Bucket: bucket,
      Key: key,
    });
  }
}