using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Renderer {

    /*
     * Three dimensional point and vector class
     */
    public class Vector3 {
        public float x, y, z;

        public Vector3(float i, float j, float k) {
            x = i;
            y = j;
            z = k;
        }

        public static Vector3 Add(Vector3 a, Vector3 b) {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 Subtract(Vector3 a, Vector3 b) {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 Unit(Vector3 a) {
            float magnitude = (float)Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
            if(magnitude == 0) {
                return a;
            }
            return new Vector3(a.x / magnitude, a.y / magnitude, a.z / magnitude);
        }

        public static Vector3 Multiply(Vector3 a, float amount) {
            return new Vector3(a.x * amount, a.y * amount, a.z * amount);
        }

        public static Vector3 Divide(Vector3 a, float amount) {
            return new Vector3(a.x / amount, a.y / amount, a.z / amount);
        }

        public static float DotProduct(Vector3 a, Vector3 b) {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3 CrossProduct(Vector3 a, Vector3 b) {
            return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public static float Distance(Vector3 a, Vector3 b) {
            return (float)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        }

        public void Add(Vector3 a) {
            x += a.x;
            y += a.y;
            z += a.z;
        }

        public void ScaleX(float amount) {
            x *= amount;
        }

        public void ScaleY(float amount) {
            y *= amount;
        }

        public void ScaleZ(float amount) {
            z *= amount;
        }

        public void Scale(float xScale, float yScale, float zScale) {
            x *= xScale;
            y *= yScale;
            z *= zScale;
        }

        public void Scale(float amount) {
            x *= amount;
            y *= amount;
            z *= amount;
        }

        public float Magnitude() {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
    }

    /*
     * Triangle class
     */
    public class Triangle {
        public Vector3[] vertices;
        public Vector3 normal;
        public Material material;

        public Triangle() {
            vertices = new Vector3[3];
            normal = null;
            material = null;
        }
    }

    /*
     * Colour class based on RGB
     */
    public class Colour {
        public int red, green, blue;

        public Colour(int r, int g, int b) {
            red = r;
            green = g;
            blue = b;
        }

        public Colour(ColourList c) {
            SetColour((int)c);
        }

        public static Colour Multiply(Colour c, float value) {
            return new Colour((int)(c.red * value), (int)(c.green * value), (int)(c.blue * value));
        }

        public static Colour Combine(Colour a, Colour b, float val) {
            return new Colour((int)(a.red * b.red * val / 255), (int)(a.green * b.green * val / 255), (int)(a.blue * b.blue * val / 255));
        }

        public static Colour Clamp(Colour c) {
            int r = c.red;
            int g = c.green;
            int b = c.blue;
            if (r > 255) {
                r = 255;
            }
            if (g > 255) {
                g = 255;
            }
            if (b > 255) {
                b = 255;
            }
            if (r < 0) {
                r = 0;
            }
            if (g < 0) {
                g = 0;
            }
            if (b < 0) {
                b = 0;
            }
            return new Colour(r, g, b);
        }

        public static Colour Add(Colour ca, Colour cb) {
            int r = ca.red + cb.red;
            int g = ca.green + cb.green;
            int b = ca.blue + cb.blue;
            return Clamp(new Colour(r, g, b));
        }

        public static Colour Add(Colour ca, Colour cb, Colour cc) {
            int r = ca.red + cb.red + cc.red;
            int g = ca.green + cb.green + cc.green;
            int b = ca.blue + cb.blue + cc.blue;
            return Clamp(new Colour(r, g, b));
        }

        public static Colour Add(Colour ca, Colour cb, Colour cc, Colour cd) {
            int r = ca.red + cb.red + cc.red + cd.red;
            int g = ca.green + cb.green + cc.green + cd.green;
            int b = ca.blue + cb.blue + cc.blue + cd.blue;
            return Clamp(new Colour(r, g, b));
        }

        public void SetColour(int r, int g, int b) {
            red = r;
            green = g;
            blue = b;
        }

        public void SetColour(int c) {
            switch(c) {
                case 0:
                    //RED
                    SetColour(255, 0, 0);
                    break;
                case 1:
                    //ORANGE
                    SetColour(255, 127, 0);
                    break;
                case 2:
                    //YELLOW
                    SetColour(255, 255, 0);
                    break;
                case 3:
                    //LIME
                    SetColour(127, 255, 0);
                    break;
                case 4:
                    //GREEN
                    SetColour(0, 255, 0);
                    break;
                case 5:
                    //OCEAN GREEN
                    SetColour(0, 255, 127);
                    break;
                case 6:
                    //CYAN
                    SetColour(0, 255, 255);
                    break;
                case 7:
                    //SKY BLUE
                    SetColour(0, 127, 255);
                    break;
                case 8:
                    //BLUE
                    SetColour(0, 0, 255);
                    break;
                case 9:
                    //PURPLE
                    SetColour(127, 0, 255);
                    break;
                case 10:
                    //PINK
                    SetColour(255, 0, 255);
                    break;
                case 11:
                    //HOT PINK
                    SetColour(255, 0, 127);
                    break;
                case 12:
                    //WHITE
                    SetColour(255, 255, 255);
                    break;
                case 13:
                    //GREY
                    SetColour(127, 127, 127);
                    break;
                case 14:
                    //BLACK
                    SetColour(0, 0, 0);
                    break;
                default:
                    //ERROR PINK
                    SetColour(255, 0, 127);
                    break;
            }
        }
    }

    /*
     * Material class with visibility properties
     */
    public class Material {
        public Colour diffuse;
        public Colour specular;
        public float diffuseVal;
        public float reflectivity;
        public float shininess;

        public Material(Colour diff, Colour spec, float diffVal, float r, float s) {
            diffuse = diff;
            specular = spec;
            diffuseVal = diffVal;
            reflectivity = r;
            shininess = s;
        }
    }

    public enum ColourList {
        RED,
        ORANGE,
        YELLOW,
        LIME,
        GREEN,
        OCEANGREEN,
        CYAN,
        SKYBLUE,
        BLUE,
        PURPLE,
        PINK,
        HOTPINK,
        WHITE,
        GREY,
        BLACK
    };

    //x and z lie flat, y is vertical
    /*
     * A three dimensional object class made up of different triangles and materials
     * The object can be moved, rotated and scaled
     */
    public class WorldObject {
        public Vector3 origin;
        public Vector3 minAABB, maxAABB;
        public Vector3[] points;
        public Vector3[] transformedPoints;
        public int[,] triangleIndices;
        public Triangle[] triangles;
        public Material[] materials;
        public float yAngle, zAngle, xAngle;
        public float scaleX, scaleY, scaleZ;

        public WorldObject(Vector3 o, Vector3[] pts, int[,] tris, Material[] mats) {
            yAngle = 0;
            zAngle = 0;
            xAngle = 0;
            scaleX = 1;
            scaleY = 1;
            scaleZ = 1;
            origin = o;
            points = pts;
            materials = mats;
            triangleIndices = tris;
            triangles = new Triangle[tris.GetLength(0)];
        }

        public void Translate(Vector3 movement) {
            origin.Add(movement);
        }

        public void MoveTo(Vector3 position) {
            origin = position;
        }

        public void RotateX(float amount) {
            xAngle += amount * (float)Math.PI / 180;
            if (xAngle > 2 * Math.PI) {
                int mult = (int)(xAngle / 2 * Math.PI);
                xAngle -= (float)(2 * Math.PI * mult);
            } else if (xAngle < -2 * Math.PI) {
                int mult = (int)(xAngle / 2 * Math.PI);
                xAngle -= (float)(2 * Math.PI * mult);
            }
        }

        public void RotateY(float amount) {
            yAngle += amount * (float)Math.PI / 180;
            if(yAngle > 2 * Math.PI) {
                int mult = (int)(yAngle / 2 * Math.PI);
                yAngle -= (float)(2 * Math.PI * mult);
            } else if (yAngle < -2 * Math.PI) {
                int mult = (int)(yAngle / 2 * Math.PI);
                yAngle -= (float)(2 * Math.PI * mult);
            }
        }

        public void RotateZ(float amount) {
            zAngle += amount * (float)Math.PI / 180;
            if (zAngle > 2 * Math.PI) {
                int mult = (int)(zAngle / 2 * Math.PI);
                zAngle -= (float)(2 * Math.PI * mult);
            } else if (zAngle < -2 * Math.PI) {
                int mult = (int)(zAngle / 2 * Math.PI);
                zAngle -= (float)(2 * Math.PI * mult);
            }
        }

        public void Scale(Vector3 scale) {
            scaleX = scale.x;
            scaleY = scale.y;
            scaleZ = scale.z;
        }

        //Applies the position, angle and scale transformations onto the original point set
        public void CalculatePoints() {
            transformedPoints = new Vector3[points.Length];
            float[] rotationCosines = new float[] {
                (float)Math.Cos(xAngle),
                (float)Math.Cos(zAngle),
                (float)Math.Cos(yAngle),
                (float)Math.Sin(xAngle),
                (float)Math.Sin(zAngle),
                (float)Math.Sin(yAngle)
            };
            for (int i = 0; i < points.Length; i++) {
                transformedPoints[i] = new Vector3(
                    origin.x + scaleX * (points[i].x * rotationCosines[1] * rotationCosines[2] - points[i].y * rotationCosines[4] + points[i].z * rotationCosines[1] * rotationCosines[5]),
                    origin.y + scaleY * (points[i].x * (rotationCosines[3] * rotationCosines[5] + rotationCosines[0] * rotationCosines[2] * rotationCosines[4]) + points[i].y * rotationCosines[0] * rotationCosines[1] + points[i].z * (rotationCosines[0] * rotationCosines[4] * rotationCosines[5] - rotationCosines[2] * rotationCosines[3])),
                    origin.z + scaleZ * (points[i].x * (rotationCosines[2] * rotationCosines[3] * rotationCosines[4] - rotationCosines[0] * rotationCosines[5]) + points[i].y * rotationCosines[1] * rotationCosines[3] + points[i].z * (rotationCosines[0] * rotationCosines[2] + rotationCosines[3] * rotationCosines[4] * rotationCosines[5]))
                );
            }
            float minX = 0, minY = 0, minZ = 0, maxX = 0, maxY = 0, maxZ = 0;
            for (int i = 0; i < transformedPoints.Length; i++) {
                if (i == 0) {
                    minX = transformedPoints[i].x;
                    minY = transformedPoints[i].y;
                    minZ = transformedPoints[i].z;
                    maxX = transformedPoints[i].x;
                    maxY = transformedPoints[i].y;
                    maxZ = transformedPoints[i].z;
                }
                if (transformedPoints[i].x < minX) {
                    minX = transformedPoints[i].x;
                }
                if (transformedPoints[i].y < minY) {
                    minY = transformedPoints[i].y;
                }
                if (transformedPoints[i].z < minZ) {
                    minZ = transformedPoints[i].z;
                }
                if (transformedPoints[i].x > maxX) {
                    maxX = transformedPoints[i].x;
                }
                if (transformedPoints[i].y > maxY) {
                    maxY = transformedPoints[i].y;
                }
                if (transformedPoints[i].z > maxZ) {
                    maxZ = transformedPoints[i].z;
                }
            }
            minAABB = new Vector3(minX, minY, minZ);
            maxAABB = new Vector3(maxX, maxY, maxZ);
        }

        //Obtains the new triangles and creates their normal vectors
        public void CalculateTriangles() {
            CalculatePoints();
            for (int i = 0; i < triangles.Length; i++) {
                Triangle t = new Triangle();
                for (int j = 0; j < 3; j++) {
                    t.vertices[j] = transformedPoints[triangleIndices[i, j]];
                }
                t.material = materials[i];
                Vector3 u = Vector3.Subtract(t.vertices[1], t.vertices[0]);
                Vector3 v = Vector3.Subtract(t.vertices[2], t.vertices[0]);
                t.normal = Vector3.Unit(Vector3.CrossProduct(u, v));
                triangles[i] = t;
            }
        }
    }

    /*
     * The world space class that contains all the objects and a single light source
     */
    public class WorldSpace {
        public List<WorldObject> objects;
        public LightSource light;
        public float ambientBrightness;

        public WorldSpace(float ambient) {
            objects = new List<WorldObject>();
            light = new LightSource(new Vector3(0, 0, 0), 0, new Colour(0, 0, 0));
            ambientBrightness = ambient;
        }

        public void AddObject(WorldObject obj) {
            objects.Add(obj);
        }

        public void SetLight(LightSource l) {
            light = l;
        }
    }

    /*
     * A light source class that can be either a point, spot or directional light
     */
    public class LightSource {
        public Vector3 origin;
        public float brightness;
        public Colour colour;

        public LightSource(Vector3 o, float b, Colour c) {
            origin = o;
            brightness = b;
            colour = c;
        }
    }

    public class PointLight : LightSource {
        public PointLight(Vector3 o, float b, Colour c) : base(o, b, c) {}
    }

    public class SpotLight : LightSource {
        public Vector3 lightVec;
        public float spotAngle;

        public SpotLight(Vector3 o, Vector3 r, float b, Colour c, float angle) : base(o, b, c) {
            lightVec = r;
            spotAngle = angle * (float)Math.PI / 180;
        }
    }

    public class DirectionalLight : LightSource {
        public Vector3 lightVec;

        public DirectionalLight(Vector3 r, float b, Colour c) : base(new Vector3(0, 0, 0), b, c) {
            lightVec = r;
        }

    }

    /*
     * This class returns information on a ray hit
     */
    public class RayHit {
        public bool hit;
        public List<Triangle> triangles;
        public List<Vector3> intersects;
        public List<float> distances;

        public RayHit() {
            hit = false;
            triangles = new List<Triangle>();
            intersects = new List<Vector3>();
            distances = new List<float>();
        }
    }

    /*
     * Creates a perspective or orthographic camera
     */
    public class Camera {
        public Vector3 origin, rotation, frustumCentre, direction;
        public float fov, near, far, aspect, screenDistance;
        public bool perspective;
        public Vector3[] corners; //top left, top right, bottom left, bottom right
        public Vector3[] transformedCorners;
        public Colour background;

        public Camera(Vector3 o, Vector3 r, float scale, float n, float f, float ratio, bool persp, Colour bg) {
            origin = o;
            rotation = r;
            rotation.x *= (float)Math.PI / 180;
            rotation.y *= (float)Math.PI / 180;
            rotation.z *= (float)Math.PI / 180;
            near = n;
            far = f;
            aspect = ratio;
            perspective = persp;
            background = bg;
            corners = new Vector3[4];
            transformedCorners = new Vector3[4];

            float halfWidth;
            float halfHeight;

            if (near < 1) {
                screenDistance = near;
            } else {
                screenDistance = 1;
            }

            if (perspective) {
                fov = scale * (float)Math.PI / 180;
                halfWidth = screenDistance * (float)Math.Tan(fov / 2);
                halfHeight = halfWidth * ratio;
            } else {
                halfWidth = scale;
                halfHeight = halfWidth * ratio;
            }

            frustumCentre = new Vector3(screenDistance, 0, 0);
            direction = new Vector3(1, 0, 0);

            corners[0] = new Vector3(screenDistance, halfHeight, -halfWidth);
            corners[1] = new Vector3(screenDistance, halfHeight, halfWidth);
            corners[2] = new Vector3(screenDistance, -halfHeight, -halfWidth);
            corners[3] = new Vector3(screenDistance, -halfHeight, halfWidth);
        }

        /*
         * Obtains the position of the screen's corners in three dimension
         */
        public void CalculateCorners() {
            float[] rotationCosines = new float[] {
                (float)Math.Cos(rotation.x),
                (float)Math.Cos(rotation.z),
                (float)Math.Cos(rotation.y),
                (float)Math.Sin(rotation.x),
                (float)Math.Sin(rotation.z),
                (float)Math.Sin(rotation.y)
            };
            for (int i = 0; i < 4; i++) {
                transformedCorners[i] = new Vector3(
                    origin.x + (corners[i].x * rotationCosines[1] * rotationCosines[2] - corners[i].y * rotationCosines[4] + corners[i].z * rotationCosines[1] * rotationCosines[5]),
                    origin.y + (corners[i].x * (rotationCosines[3] * rotationCosines[5] + rotationCosines[0] * rotationCosines[2] * rotationCosines[4]) + corners[i].y * rotationCosines[0] * rotationCosines[1] + corners[i].z * (rotationCosines[0] * rotationCosines[4] * rotationCosines[5] - rotationCosines[2] * rotationCosines[3])),
                    origin.z + (corners[i].x * (rotationCosines[2] * rotationCosines[3] * rotationCosines[4] - rotationCosines[0] * rotationCosines[5]) + corners[i].y * rotationCosines[1] * rotationCosines[3] + corners[i].z * (rotationCosines[0] * rotationCosines[2] + rotationCosines[3] * rotationCosines[4] * rotationCosines[5]))
                );
            }
            frustumCentre = Vector3.Add(transformedCorners[0], Vector3.Add(Vector3.Divide(Vector3.Subtract(transformedCorners[1], transformedCorners[0]), 2), Vector3.Divide(Vector3.Subtract(transformedCorners[2], transformedCorners[0]), 2)));
            direction = Vector3.Unit(Vector3.Subtract(frustumCentre, origin));
        }

        /*
         * Checks to see if the current ray is intersecting a triangle
         * This is where the most optimization is required
         */
        public RayHit GetRayHit(WorldSpace world, Vector3 start, Vector3 ray, Triangle exclusionTriangle) {
            RayHit h = new RayHit();
            for (int i = 0; i < world.objects.Count; i++) {
                //Checks to see if the ray is within an object's AABB
                Vector3 minAABB = world.objects[i].minAABB;
                Vector3 maxAABB = world.objects[i].maxAABB;
                float tx1 = (minAABB.x - start.x) / ray.x;
                float tx2 = (maxAABB.x - start.x) / ray.x;
                float ty1 = (minAABB.y - start.y) / ray.y;
                float ty2 = (maxAABB.y - start.y) / ray.y;
                float tz1 = (minAABB.z - start.z) / ray.z;
                float tz2 = (maxAABB.z - start.z) / ray.z;
                float tmin, tmax;
                if(tx1 < tx2) {
                    tmin = tx1;
                    tmax = tx2;
                } else {
                    tmin = tx2;
                    tmax = tx1;
                }
                float tmpmin, tmpmax;
                if(ty1 < ty2) {
                    tmpmin = ty1;
                    tmpmax = ty2;
                } else {
                    tmpmin = ty2;
                    tmpmax = ty1;
                }
                if(tmax < tmpmin) {
                    tmpmin = tmax;
                }
                if(tmin > tmpmax) {
                    tmpmax = tmin;
                }
                if(tmin < tmpmin) {
                    tmin = tmpmin;
                }
                if(tmax > tmpmax) {
                    tmax = tmpmax;
                }
                if (tz1 < tz2) {
                    tmpmin = tz1;
                    tmpmax = tz2;
                } else {
                    tmpmin = tz2;
                    tmpmax = tz1;
                }
                if (tmax < tmpmin) {
                    tmpmin = tmax;
                }
                if (tmin > tmpmax) {
                    tmpmax = tmin;
                }
                if (tmin < tmpmin) {
                    tmin = tmpmin;
                }
                if (tmax > tmpmax) {
                    tmax = tmpmax;
                }
                if(tmax == tmin) {
                    continue;
                }
                //Checks each triangle for intersection with vectors
                for (int j = 0; j < world.objects[i].triangles.Length; j++) {
                    Triangle currentTriangle = world.objects[i].triangles[j];
                    if (currentTriangle == exclusionTriangle) {
                        continue;
                    }
                    if(Vector3.DotProduct(currentTriangle.normal, ray) >= 0) {
                        continue;
                    }
                    float npb = Vector3.DotProduct(currentTriangle.normal, Vector3.Subtract(start, currentTriangle.vertices[1]));
                    float nr = Vector3.DotProduct(currentTriangle.normal, ray);
                    float t = 0; // distance
                    if (nr != 0) {
                        t = -npb / nr;
                    }
                    if(t <= 0) {
                        continue;
                    }
                    Vector3 ab = Vector3.Subtract(currentTriangle.vertices[0], currentTriangle.vertices[1]);
                    Vector3 cb = Vector3.Subtract(currentTriangle.vertices[2], currentTriangle.vertices[1]);
                    Vector3 ac = Vector3.Subtract(currentTriangle.vertices[0], currentTriangle.vertices[2]);
                    Vector3 intersect = Vector3.Add(start, Vector3.Multiply(ray, t));
                    Vector3 da = Vector3.Subtract(intersect, currentTriangle.vertices[0]);
                    Vector3 db = Vector3.Subtract(intersect, currentTriangle.vertices[1]);
                    Vector3 dc = Vector3.Subtract(intersect, currentTriangle.vertices[2]);
                    //Checks to see if there is a point of intersection
                    if (Vector3.DotProduct(currentTriangle.normal, Vector3.CrossProduct(da, ab)) >= 0 && Vector3.DotProduct(currentTriangle.normal, Vector3.CrossProduct(cb, db)) >= 0 && Vector3.DotProduct(currentTriangle.normal, Vector3.CrossProduct(ac, dc)) >= 0) {
                        h.hit = true;
                        h.triangles.Add(currentTriangle);
                        h.intersects.Add(intersect);
                        h.distances.Add(t);
                    }
                }
            }
            return h;
        }

        /*
         * Checks for intersections only if they are within a certain range of the camera
         */
        public RayHit GetRayHitClipping(WorldSpace world, Vector3 start, Vector3 ray, float near, float far) {
            RayHit h = GetRayHit(world, start, ray, null);
            RayHit newH = new RayHit();
            for(int i = 0; i < h.intersects.Count; i++) {
                float perpDistance = h.distances[i];
                if (perspective) {
                    perpDistance *= Vector3.DotProduct(ray, direction);
                }
                if (perpDistance >= near - screenDistance && perpDistance <= far - screenDistance) {
                    newH.hit = true;
                    newH.intersects.Add(h.intersects[i]);
                    newH.triangles.Add(h.triangles[i]);
                    newH.distances.Add(h.distances[i]);
                }
            }
            return newH;
        }

        /*
         * Returns the colour that would be seen for a specific ray trace
         */
        public Colour RayTrace(WorldSpace world, Vector3 viewVector, Vector3 intersect, Triangle triangle, int reflectiveBounces) {
            RayHit reflectHit;
            Colour diffuse;
            Vector3 lightRay;
            float lightCos;
            if (world.light.GetType() == typeof(DirectionalLight)) {
                //Directional lighting
                DirectionalLight worldLight = (DirectionalLight)world.light;
                lightRay = Vector3.Unit(Vector3.Multiply(worldLight.lightVec, -1));
                lightCos = Math.Abs(Vector3.DotProduct(lightRay, triangle.normal));
                reflectHit = GetRayHit(world, intersect, lightRay, triangle);
                diffuse = Colour.Combine(triangle.material.diffuse, world.light.colour, world.light.brightness * lightCos * triangle.material.diffuseVal);
            } else if (world.light.GetType() == typeof(SpotLight)) {
                //Spot lighting
                SpotLight worldLight = (SpotLight)world.light;
                Vector3 spotVector = Vector3.Unit(Vector3.Multiply(worldLight.lightVec, -1));
                lightRay = Vector3.Subtract(world.light.origin, intersect);
                float lightDistance = lightRay.Magnitude();
                lightRay = Vector3.Unit(lightRay);
                lightCos = Math.Abs(Vector3.DotProduct(lightRay, triangle.normal));
                if(Vector3.DotProduct(spotVector, lightRay) >= Math.Cos(worldLight.spotAngle)) {
                    reflectHit = GetRayHit(world, intersect, lightRay, triangle);
                    diffuse = Colour.Combine(triangle.material.diffuse, world.light.colour, world.light.brightness * lightCos * triangle.material.diffuseVal / (lightDistance * lightDistance));
                } else {
                    reflectHit = new RayHit();
                    reflectHit.hit = true;
                    diffuse = new Colour(ColourList.BLACK);
                }
            } else {
                //Point lighting
                lightRay = Vector3.Subtract(world.light.origin, intersect);
                float lightDistance = lightRay.Magnitude();
                lightRay = Vector3.Unit(lightRay);
                lightCos = Math.Abs(Vector3.DotProduct(lightRay, triangle.normal));
                reflectHit = GetRayHit(world, intersect, lightRay, triangle);
                diffuse = Colour.Combine(triangle.material.diffuse, world.light.colour, world.light.brightness * lightCos * triangle.material.diffuseVal / (lightDistance * lightDistance));
            }
            Colour ambient = Colour.Combine(triangle.material.diffuse, background, world.ambientBrightness);
            Colour reflection = new Colour(ColourList.BLACK);
            Colour specular = new Colour(ColourList.BLACK);
            if(reflectiveBounces > 0 && triangle.material.reflectivity > 0) {
                //Checks for recursive reflectivity
                Vector3 reverseVector = Vector3.Unit(Vector3.Multiply(viewVector, -1));
                Vector3 reflectance = Vector3.Subtract(Vector3.Multiply(triangle.normal, 2 * Vector3.DotProduct(triangle.normal, reverseVector)), reverseVector);
                Vector3 lightReflectance = Vector3.Subtract(Vector3.Multiply(triangle.normal, 2 * Vector3.DotProduct(triangle.normal, lightRay)), lightRay);
                float specularDot = Vector3.DotProduct(lightReflectance, reverseVector);
                if(specularDot < 0) {
                    specularDot = 0;
                }
                specular = Colour.Combine(triangle.material.specular, world.light.colour, triangle.material.reflectivity * (float)Math.Pow(specularDot, triangle.material.shininess));
                RayHit reflectionHit = GetRayHit(world, intersect, reflectance, triangle);
                if (reflectionHit.hit) {
                    float smallestDistance = reflectionHit.distances[0];
                    Vector3 reflectionIntersect = reflectionHit.intersects[0];
                    Triangle reflectionTriangle = reflectionHit.triangles[0];
                    for (int k = 1; k < reflectionHit.intersects.Count; k++) {
                        if (reflectionHit.distances[k] < smallestDistance) {
                            smallestDistance = reflectionHit.distances[k];
                            reflectionIntersect = reflectionHit.intersects[k];
                            reflectionTriangle = reflectionHit.triangles[k];
                        }
                    }
                    reflection = Colour.Add(reflection, Colour.Multiply(RayTrace(world, reflectance, reflectionIntersect, reflectionTriangle, reflectiveBounces - 1), triangle.material.reflectivity));
                }
            }
            if (reflectHit.hit) {
                return Colour.Add(ambient, reflection);
            } else {
                return Colour.Add(ambient, diffuse, reflection, specular);
            }
        }

        /*
         * Returns an array of colours representing the output image
         */
        public Colour[,] GetImage(WorldSpace world, int width, int height, int reflectiveBounces = 4) {
            CalculateCorners();
            for(int i = 0; i < world.objects.Count; i++) {
                world.objects[i].CalculateTriangles();
            }
            Colour[,] image = new Colour[width, height];
            Vector3 deltaWidth = Vector3.Divide(Vector3.Subtract(transformedCorners[1], transformedCorners[0]), width);
            Vector3 deltaHeight = Vector3.Divide(Vector3.Subtract(transformedCorners[2], transformedCorners[0]), height);
            for(int i = 0; i < height; i++) {
                Vector3 heightPoint = Vector3.Add(transformedCorners[0], Vector3.Multiply(deltaHeight, i));
                for(int j = 0; j < width; j++) {
                    Vector3 point = Vector3.Add(heightPoint, Vector3.Multiply(deltaWidth, j));
                    Vector3 rayDir;
                    if(perspective) {
                        rayDir = Vector3.Unit(Vector3.Subtract(point, origin));
                    } else {
                        rayDir = direction;
                    }
                    RayHit camHit = GetRayHitClipping(world, point, rayDir, near, far);
                    if(camHit.hit) {
                        float smallestDistance = camHit.distances[0];
                        Vector3 closestIntersect = camHit.intersects[0];
                        Triangle triangle = camHit.triangles[0];
                        for(int k = 1; k < camHit.intersects.Count; k++) {
                            if(camHit.distances[k] < smallestDistance) {
                                smallestDistance = camHit.distances[k];
                                closestIntersect = camHit.intersects[k];
                                triangle = camHit.triangles[k];
                            }
                        }
                        image[j, i] = RayTrace(world, rayDir, closestIntersect, triangle, reflectiveBounces);
                    } else {
                        image[j, i] = background;
                    }
                }
            }
            return image;
        }
    }

    /*
     * Importer class that imports ASCII type STL files
     */
    public class Importer {
        public Importer() {

        }

        public WorldObject ImportSTL(Vector3 origin, Material mat, string filename) {
            if(filename[filename.Length - 4] == '.') {
                filename = filename.Remove(filename.Length - 4);
            }
            string[] stlFile = File.ReadAllLines("../" + filename + ".stl");
            int currentLine = 1;
            int numTriangles = (stlFile.Length - 2) / 7;
            Vector3[] norms = new Vector3[numTriangles];
            Vector3[] points = new Vector3[numTriangles * 3];
            int[,] tris = new int[numTriangles, 3];
            Material[] mats = new Material[numTriangles];
            for (int i = 0; i < numTriangles; i++) {
                string[] normals = stlFile[currentLine].Split(' ');
                norms[i] = new Vector3(float.Parse(normals[2]), float.Parse(normals[3]), float.Parse(normals[4]));
                currentLine += 2;
                for (int j = 0; j < 3; j++) {
                    string[] vertices = stlFile[currentLine + j].Split(' ');
                    points[i * 3 + j] = new Vector3(float.Parse(vertices[1]), float.Parse(vertices[2]), float.Parse(vertices[3]));
                    tris[i, j] = i * 3 + j;
                }
                mats[i] = mat;
                currentLine += 5;
            }
            WorldObject obj = new WorldObject(origin, points, tris, mats);
            return obj;
        }
    }

    /*
     * Exporter class that exports ppm files
     */
    public class Exporter {
        public int width, height;
        public Colour[,] image;

        public Exporter(int w, int h) {
            width = w;
            height = h;
            image = new Colour[w, h];
        }

        public void SetImage(Colour[,] img) {
            image = img;
        }

        public void CreateFile(string filename) {
            StringBuilder fileOutput = new StringBuilder();
            string output = "P3\n";
            output += width.ToString() + " " + height.ToString() + "\n255\n";
            fileOutput.Append(output);
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    fileOutput.Append(image[j, i].red.ToString());
                    fileOutput.Append(" ");
                    fileOutput.Append(image[j, i].green.ToString());
                    fileOutput.Append(" ");
                    fileOutput.Append(image[j, i].blue.ToString());
                    if (j != width - 1) {
                        fileOutput.Append(" ");
                    }
                }
                if (i != height - 1) {
                    fileOutput.Append("\n");
                }
            }
            File.WriteAllText("../" + filename + ".ppm", fileOutput.ToString());
        }
    }

    /*
     * Main function
     */
    class Raytracer {
        static void Main(string[] args) {
            int width = 1280;
            int height = 720;
            string filename = "image";
            Console.WriteLine("Importing models...\t 0% complete");
            Importer importer = new Importer();
            Console.WriteLine("Generating scene...\t25% complete");
            Camera camera = new Camera(new Vector3(0, 3, 0), new Vector3(0, 0, -15), 80.0f, 1.0f, 40.0f, (float)height / width, true, new Colour(155, 230, 255));
            WorldSpace world = new WorldSpace(0.15f);
            Material orangeFlat = new Material(new Colour(ColourList.ORANGE), new Colour(ColourList.WHITE), 1.0f, 0.0f, 0);
            Material greenShiny = new Material(new Colour(ColourList.YELLOW), new Colour(ColourList.WHITE), 1.0f, 0.5f, 5);
            Material floorGrey = new Material(new Colour(ColourList.GREY), new Colour(ColourList.WHITE), 1.0f, 0.5f, 20);
            Material glossPurple = new Material(new Colour(ColourList.PURPLE), new Colour(ColourList.WHITE), 1.0f, 0.9f, 3);
            WorldObject pyramid = new WorldObject(
                new Vector3(5, 1.5f, -1.5f),
                new Vector3[] {
                    new Vector3(0, -1, 0),
                    new Vector3(1, 1, -1),
                    new Vector3(1, 1, 1),
                    new Vector3(-1, 1, 0)
                },
                new int[,] {
                    {0, 1, 2},
                    {0, 3, 1},
                    {0, 2, 3},
                    {3, 2, 1}
                },
                new Material[] {
                    greenShiny,
                    orangeFlat,
                    greenShiny,
                    orangeFlat
                }
            );
            WorldObject sphere = importer.ImportSTL(
                new Vector3(6, 1.5f, 0),
                glossPurple,
                "sphere.stl"
            );
            WorldObject floor = new WorldObject(
                new Vector3(0, 0, 0),
                new Vector3[] {
                    new Vector3(0, 0, -20),
                    new Vector3(0, 0, 20),
                    new Vector3(40, 0, -20),
                    new Vector3(40, 0, 20),
                    new Vector3(0, -1, 0)
                },
                new int[,] {
                    {0, 1, 2},
                    {1, 3, 2}
                },
                new Material[] {
                    floorGrey,
                    floorGrey
                }
            );
            pyramid.RotateZ(45);
            LightSource light = new PointLight(new Vector3(3.0f, 5.0f, 1.0f), 30.0f, new Colour(255, 247, 219));
            //LightSource light = new SpotLight(new Vector3(3.0f, 5.0f, 1.0f), new Vector3(1, -1, 0), 60.0f, new Colour(36, 36, 36), 30.0f);
            //LightSource light = new DirectionalLight(new Vector3(1, -1, -1), 0.7f, new Colour(255, 247, 219));
            world.AddObject(pyramid);
            world.AddObject(sphere);
            world.AddObject(floor);
            world.SetLight(light);
            Exporter exporter = new Exporter(width, height);
            Colour[,] image = new Colour[width, height];
            Console.WriteLine("Rendering image...\t50% complete");
            exporter.SetImage(camera.GetImage(world, width, height, 8));
            Console.WriteLine("Exporting...\t\t75% complete");
            exporter.CreateFile(filename);
            Console.WriteLine("All jobs finished\t100% complete\n");
            Console.WriteLine("Exported file " + filename + ".ppm");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}