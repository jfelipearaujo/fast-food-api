import http from 'k6/http';
import { check, fail, sleep } from 'k6';

import { Trend, Rate } from 'k6/metrics';

export const duration = new Trend('get_all_products_duration', true);
export const failRate = new Rate('get_all_products_fail_rate');
export const successRate = new Rate('get_all_products_success_rate');
export const reqRate = new Rate('get_all_products_req_rate');

export default function () {
    const host = 'http://localhost:30002/api/v1';
    const route = '/products';

    const res = http.get(`${host}${route}`);

    duration.add(res.timings.duration);
    reqRate.add(1);

    const errMsg = `Unexpected status code (expected 200, got ${res.status}) for GET ${host}${route}`;

    if (check(res, { 'status is 200': r => r.status === 200 })) {
        console.log(`GET ${host}${route} OK`);
        successRate.add(1);
    } else {
        console.log(`GET ${host}${route} FAILED`);
        failRate.add(1);

        fail(errMsg);
    }

    sleep(1);
}