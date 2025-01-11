import { RequestPaginationDto } from "../common/requestPaginationDto";

export interface RequestUserChatMessageDto extends RequestPaginationDto {
    userChatId: number
}

export interface GetUserChatMessageDto {
    from: string
    isFromMe: boolean
    to: string
    message: string
    createdDate: string
    isSeen: boolean
}

export interface AddMessageDto {
    userChatId: number
    toUserId: number
    message: string
}


export interface NotifyUserMessageDto {
    userChatId: number
    messageId: number
    message: string
    fromFullName: string
    toUserId: number
    createdDate: string
}