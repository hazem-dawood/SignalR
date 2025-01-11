export interface ResultDto<T> {
    isSuccess: boolean
    messages: string[]
    data: T | null
}