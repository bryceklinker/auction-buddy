export interface AuthenticationResultDto {
    isSuccess: boolean;
    accessToken?: string;
    expiresIn?: number;
    tokenType?: string;
}