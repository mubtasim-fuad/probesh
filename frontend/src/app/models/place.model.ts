export type PlaceCategory =
  | 'Healthcare'
  | 'Education'
  | 'Government'
  | 'Transport'
  | 'Restaurant'
  | 'Shopping'
  | 'Recreation'
  | 'Other';

export type VerificationStatus = 'PendingReview' | 'Verified' | 'Rejected';

export type AccessibilityLevel =
  | 'Unknown'
  | 'Limited'
  | 'PartiallyAccessible'
  | 'HighlyAccessible';

export interface AccessibilityAudit {
  stepFreeEntrance: boolean | null;
  accessibleToilet: boolean | null;
  elevator: boolean | null;
  wideDoorways: boolean | null;
  accessibleParking: boolean | null;
  staffAssistance: boolean | null;
  score: number;
  knownFeatureCount: number;
  level: AccessibilityLevel;
  notes: string | null;
  evidencePhotoUrl: string | null;
  createdAtUtc: string;
}

export interface Place {
  id: string;
  name: string;
  category: PlaceCategory;
  address: string;
  area: string;
  city: string;
  latitude: number;
  longitude: number;
  description: string | null;
  verificationStatus: VerificationStatus;
  createdAtUtc: string;
  audit: AccessibilityAudit;
}

export interface PlaceSummary {
  totalPlaces: number;
  verifiedPlaces: number;
  pendingPlaces: number;
  highlyAccessiblePlaces: number;
  averageScore: number;
}

export interface AccessibilityAuditRequest {
  stepFreeEntrance: boolean | null;
  accessibleToilet: boolean | null;
  elevator: boolean | null;
  wideDoorways: boolean | null;
  accessibleParking: boolean | null;
  staffAssistance: boolean | null;
  notes: string | null;
  evidencePhotoUrl: string | null;
}

export interface CreatePlaceRequest {
  name: string;
  category: PlaceCategory;
  address: string;
  area: string;
  city: string;
  latitude: number;
  longitude: number;
  description: string | null;
  audit: AccessibilityAuditRequest;
}

export const PLACE_CATEGORIES: readonly PlaceCategory[] = [
  'Healthcare',
  'Education',
  'Government',
  'Transport',
  'Restaurant',
  'Shopping',
  'Recreation',
  'Other',
];

export const ACCESSIBILITY_LEVELS: readonly AccessibilityLevel[] = [
  'HighlyAccessible',
  'PartiallyAccessible',
  'Limited',
  'Unknown',
];
