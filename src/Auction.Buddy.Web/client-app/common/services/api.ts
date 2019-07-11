async function post<TBody, TResult>(url: string, data: TBody, headers: HeadersInit = {}): Promise<TResult> {
    return fetcher<TResult>(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            ...headers,
            'Content-Type': 'application/json',
        },
    });
}

async function get<TResult>(url: string, headers: HeadersInit = {}): Promise<TResult> {
    return await fetcher<TResult>(url, {
        headers,
    });
}

async function fetcher<TResult>(url: string, init?: RequestInit): Promise<TResult> {
    const response = await fetch(url, init);

    if (response.ok) {
        return await response.json();
    }

    throw new Error(await response.text());
}

export interface ApiService {
    post: <TBody, TResult>(url: string, data: TBody, headers?: HeadersInit) => Promise<TResult>;
    get: <TResult>(url: string, headers?: HeadersInit) => Promise<TResult>;
}

export const api: ApiService = { post, get };
