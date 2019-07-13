export interface AuthenticationResultDto {
    isSuccess: boolean;
    accessToken?: string;
    expiresIn?: number;
    expiresAt?: string;
    tokenType?: string;
}
