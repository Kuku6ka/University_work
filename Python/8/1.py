from dsmltf import Counter, defaultdict, partial
from math import log

def entropy(class_probabilities):
    return sum(-p*log(p,2) for p in class_probabilities if p)

def class_probabilities(labels):
    total_count = len(labels)
    return [count/total_count for count in Counter(labels).values()]

def data_entropy(labeled_data):
    labels = [label for _,label in labeled_data]
    probabilities = class_probabilities(labels)
    return entropy(probabilities)

def partition_entropy(subsets):
    total_count = sum(len(subset) for subset in subsets)
    return sum(data_entropy(subset) * len(subset) / total_count for subset in subsets)

def partition_by(inputs,attribute):
    groups = defaultdict(list)
    for inp in inputs:
        key = inp[0][attribute]
        groups[key].append(inp)
    return groups

def partition_entropy_by(inputs,attribute):
    partitions = partition_by(inputs,attribute)
    return partition_entropy(partitions.values())

def build_tree_id3(inputs, split_candidates=None):
    if split_candidates is None:
        split_candidates = inputs[0][0].keys()
    num_inputs = len(inputs)
    num_trues = len([label for item, label in inputs if label])
    num_falses = num_inputs - num_trues
    if num_trues == 0: return False
    if num_falses == 0: return True
    if not split_candidates:
        return num_trues >= num_falses
    best_attribute = min(split_candidates, key = partial(partition_entropy_by, inputs))
    partitions = partition_by(inputs, best_attribute)
    new_candidates = [a for a in split_candidates if a != best_attribute]
    subtrees = {attribute_value : build_tree_id3(subset, new_candidates) for attribute_value,subset in iter(partitions.items())}
    subtrees[None] = num_trues > num_falses
    return (best_attribute, subtrees)

def classify(tree, inp):
    if tree in (True, False): return tree
    attribute, subtree_dict = tree
    subtree_key = inp.get(attribute)
    if subtree_key not in subtree_dict: subtree_key = None
    subtree = subtree_dict[subtree_key]
    return classify (subtree, inp)

def main():
    inputs_tren = [
        ({'specialization': '090301', 'has_recent_debt': 'yes',
          'attendance_class': '30%-50%', 'registered_vk': 'yes', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'yes', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '090304', 'has_recent_debt': 'no',
          'attendance_class': '<30%', 'registered_vk': 'no', 'registered_classroom': 'yes',
          'athlete': 'yes', 'student_events': 'no', 'teacher_feedback': 'хорошо'}, False),

        ({'specialization': '100503', 'has_recent_debt': 'no',
          'attendance_class': '50%-80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'отлично'}, True),

        ({'specialization': '090301', 'has_recent_debt': 'yes',
          'attendance_class': '<30%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '090304', 'has_recent_debt': 'no',
          'attendance_class': '50%-80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'yes', 'student_events': 'yes', 'teacher_feedback': 'отлично'}, True),

        ({'specialization': '100503', 'has_recent_debt': 'yes',
          'attendance_class': '30%-50%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'yes', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '090301', 'has_recent_debt': 'no',
          'attendance_class': '50%-80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'no', 'student_events': 'yes', 'teacher_feedback': 'хорошо'}, True),

        ({'specialization': '090304', 'has_recent_debt': 'no',
          'attendance_class': '>80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'yes', 'student_events': 'yes', 'teacher_feedback': 'отлично'}, True),

        ({'specialization': '100503', 'has_recent_debt': 'yes',
          'attendance_class': '<30%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '090301', 'has_recent_debt': 'no',
          'attendance_class': '50%-80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'хорошо'}, True),

        ({'specialization': '090304', 'has_recent_debt': 'yes',
          'attendance_class': '30%-50%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'yes', 'student_events': 'no', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '100503', 'has_recent_debt': 'no',
          'attendance_class': '>80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'yes', 'student_events': 'yes', 'teacher_feedback': 'отлично'}, True),

        ({'specialization': '090301', 'has_recent_debt': 'no',
          'attendance_class': '50%-80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'no', 'student_events': 'yes', 'teacher_feedback': 'хорошо'}, True),

        ({'specialization': '090304', 'has_recent_debt': 'yes',
          'attendance_class': '<30%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '100503', 'has_recent_debt': 'no',
          'attendance_class': '30%-50%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'yes', 'student_events': 'no', 'teacher_feedback': 'хорошо'}, True),

        ({'specialization': '090301', 'has_recent_debt': 'no',
          'attendance_class': '>80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'no', 'student_events': 'yes', 'teacher_feedback': 'отлично'}, True),

        ({'specialization': '090304', 'has_recent_debt': 'yes',
          'attendance_class': '50%-80%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'yes', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '100503', 'has_recent_debt': 'no',
          'attendance_class': '50%-80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'yes', 'student_events': 'yes', 'teacher_feedback': 'отлично'}, True),

        ({'specialization': '090301', 'has_recent_debt': 'no',
          'attendance_class': '<30%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '090304', 'has_recent_debt': 'yes',
          'attendance_class': '30%-50%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'плохо'}, False),

        ({'specialization': '100503', 'has_recent_debt': 'no',
          'attendance_class': '>80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'yes', 'student_events': 'yes', 'teacher_feedback': 'хорошо'}, True),

        ({'specialization': '090301', 'has_recent_debt': 'no',
          'attendance_class': '50%-80%', 'registered_vk': 'yes', 'registered_classroom': 'yes',
          'athlete': 'no', 'student_events': 'yes', 'teacher_feedback': 'хорошо'}, True),

        ({'specialization': '090304', 'has_recent_debt': 'yes',
          'attendance_class': '<30%', 'registered_vk': 'no', 'registered_classroom': 'no',
          'athlete': 'no', 'student_events': 'no', 'teacher_feedback': 'плохо'}, False),
    ]

    tree = build_tree_id3(inputs_tren)
    print(tree)

    counter = 0
    for label in inputs_tren:
        result = classify(tree, label[0])
        print(result)
        if result == label[1]:
            counter += 1
    print(f"Из {len(inputs_tren)} правильно определено {counter}")


if __name__ == '__main__':
    main()