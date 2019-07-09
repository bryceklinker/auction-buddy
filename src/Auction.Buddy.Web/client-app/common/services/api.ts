async function post<TBody, TResult>(url: string, data: TBody, headers: HeadersInit = {}): Promise<TResult> {
    const response = await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            ...headers,
            'Content-Type': 'application/json'
        }
    });
    if (response.ok) {
        return await response.json();
    }
    
    throw new Error(await response.text());
}

async function get<TResult>(url: string, headers: HeadersInit = {}): Promise<TResult> {
    const response = await fetch(url, { headers: headers });
    return await response.json();
}

export interface ApiService {
    post: <TBody, TResult>(url: string, data: TBody, headers?: HeadersInit) => Promise<TResult>;
    get: <TResult>(url: string, headers?: HeadersInit) => Promise<TResult>;
}

export const api: ApiService = { post, get };