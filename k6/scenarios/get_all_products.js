import http from 'k6/http';
import { check, fail, sleep } from 'k6';

import { Trend, Rate } from 'k6/metrics';

export const GetAllProductsDuration = new Trend('get_all_products_duration', true);
export const GetAllProductsFailRate = new Rate('get_all_products_fail_rate');
export const GetAllProductsSuccessRate = new Rate('get_all_products_success_rate');
export const GetAllProductsReqRate = new Rate('get_all_products_req_rate');

export default function () {
    const host = 'http://localhost:30002/api/v1';

    const res = http.get(`${host}/products`);

    GetAllProductsDuration.add(res.timings.duration);
    GetAllProductsReqRate.add(1);

    const errMsg = `Unexpected status code (expected 200, got ${res.status}) for GET ${host}/products`;

    if (check(res, { 'status is 200': r => r.status === 200 })) {
        console.log(`GET ${host}/products OK`);
        GetAllProductsSuccessRate.add(1);
    } else {
        console.log(`GET ${host}/products FAILED`);
        GetAllProductsFailRate.add(1);
        
        fail(errMsg);
    }

    sleep(1);
}